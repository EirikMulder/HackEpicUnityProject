using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;

public class satOrbital : MonoBehaviour
{

    private Rigidbody rigidBody;
    public LineRenderer trajectoryLookahead;
    public float timestep = 10;
    public float lookaheadTime = 3600;
    private Queue<Action> runInMain = new();
    private bool integrating = false;

    // Start is called before the first frame update
    void Start()
    {
        var G = SceneController.Instance.G;
        var massJupiter = SceneController.Instance.massJupiter;
        var systemScale = SceneController.Instance.systemScale;

        rigidBody = GetComponent<Rigidbody>();
        float velocityMagnitude = Mathf.Sqrt(SceneController.Instance.timeScale * G * massJupiter / transform.position.magnitude / systemScale);
        velocityMagnitude *= systemScale * systemScale;
        Vector3 initialVelocity = Vector3.Cross(transform.position, Vector3.up);
        initialVelocity = initialVelocity.normalized * velocityMagnitude;
        rigidBody.velocity = initialVelocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var G = SceneController.Instance.G;
        var massJupiter = SceneController.Instance.massJupiter;
        var systemScale = SceneController.Instance.systemScale;

        // Vector3 unitR = transform.position.normalized;
        // float rMag = transform.position.magnitude / systemScale;
        //
        // Vector3 force = G * rigidBody.mass * massJupiter / (rMag * rMag) * -unitR;
        // force *= systemScale;

        Vector3 force = SceneController.Instance.CalculateAcceleration(transform.position, SceneController.Instance.attractorTuples) * rigidBody.mass;

        // Debug.Log($"Force: {force}, mag: {force.magnitude}");

        rigidBody.AddForce(force);

        // var lookaheadPositions = Integrate(transform.position, rigidBody.velocity, timestep, lookaheadTime);

        if (runInMain.Count > 0)
        {
            var stuff = runInMain.Dequeue();
            stuff();
        }

        if (!integrating)
        {
            // Task.Run(() => IntegrateToMainThread(transform.position, rigidBody.position));
            // Debug.Log($"Task.Run!");
            IntegrateToMainThread(transform.position, rigidBody.velocity, SceneController.Instance.attractorTuples);
        }
        else
        {
            // Debug.Log($"integrating!");
        }

        // Debug.Log($"Jupiter: {force.magnitude}");
        // Debug.Log($"Europa: {force2.magnitude}");
    }

    async void IntegrateToMainThread(Vector3 startPos, Vector3 startVel, List<Tuple<Vector3, float>> attractorTuples)
    {
        integrating = true;
        // Debug.Log($"Began Integrating!");
        // var lookaheadPositions = Integrate(startPos, startVel, timestep, lookaheadTime);
        await Task.Run(() => Integrate(startPos, startVel, timestep, lookaheadTime, attractorTuples));
        // Debug.Log($"Finished Integrating");
        integrating = false;
    }

    void Integrate(Vector3 startPos, Vector3 startVel, float dt, float tf, List<Tuple<Vector3, float>> attractorTuples)
    {
        var inst = SceneController.Instance;
        List<Vector3> positions = new();
        positions.Add(startPos);
        var stateVector = new StateVector(startPos, startVel);
        // Debug.Log($"State Vector Len: {stateVector.statePosVector}, {stateVector.stateVelVector}");
        for (float t = 0; t < tf; t += dt)
        {
            Vector3 acceleration1 = inst.CalculateAcceleration(stateVector.GetPosition(), attractorTuples);
            var k1 = new StateVector(stateVector, acceleration1, dt);
            Vector3 acceleration2 = inst.CalculateAcceleration(stateVector.GetPosition() + (dt / 2 * k1.GetPosition()), attractorTuples);
            var k2 = new StateVector(stateVector, acceleration2, dt);
            Vector3 acceleration3 = inst.CalculateAcceleration(stateVector.GetPosition() + (dt / 2 * k2.GetPosition()), attractorTuples);
            var k3 = new StateVector(stateVector, acceleration3, dt);
            Vector3 acceleration4 = inst.CalculateAcceleration(stateVector.GetPosition() + (dt * k3.GetPosition()), attractorTuples);
            var k4 = new StateVector(stateVector, acceleration3, dt);

            stateVector = stateVector + (1 / 6f) * (k1 + (2 * k2) + (2 * k3) + k4);
            positions.Add(stateVector.GetPosition());

            dt = positions.Last().magnitude / 5f * 0.01f;
            dt = Mathf.Clamp(dt, 0.005f, 2f);
        }

        var lookaheadPositions = positions;
        runInMain.Enqueue(
            new Action(() =>
            {
                trajectoryLookahead.positionCount = lookaheadPositions.Count;
                trajectoryLookahead.SetPositions(lookaheadPositions.ToArray());
                // Debug.Log($"Updated Positions");
            }));

        // return positions;
    }
}

class StateVector
{
    public System.Numerics.Vector3 statePosVector;
    public System.Numerics.Vector3 stateVelVector;

    public StateVector(System.Numerics.Vector3 newPosVector, System.Numerics.Vector3 newVelVector)
    {
        statePosVector = newPosVector;
        stateVelVector = newVelVector;
    }

    public StateVector(Vector3 position, Vector3 velocity)
    {
        statePosVector = new(position.x, position.y, position.z);
        stateVelVector = new (velocity.x, velocity.y, velocity.z);
    }

    public StateVector(StateVector currentState, Vector3 acceleration, float timestep)
    {
        currentState = timestep * currentState;
        acceleration = timestep * acceleration;
        statePosVector = new(currentState[3], currentState[4], currentState[5]);
        stateVelVector = new(acceleration.x, acceleration.y, acceleration.z);
    }

    public float this[int i]
    {
        get
        {
            return (new float[] { statePosVector.X, statePosVector.Y, statePosVector.Z, stateVelVector.X, stateVelVector.Y, stateVelVector.Z })[i];
        }
    }

    public static StateVector operator+(StateVector v1, StateVector v2)
    {
        return new(v1.statePosVector + v2.statePosVector, v1.stateVelVector + v2.stateVelVector);
    }

    public static StateVector operator*(float c, StateVector v2)
    {
        return new(c * v2.statePosVector, c * v2.stateVelVector);
    }

    public Tuple<Vector3, Vector3> ToPosAndVel()
    {
        Vector3 position = new Vector3(statePosVector.X, statePosVector.Y, statePosVector.Z);
        Vector3 velocity = new Vector3(stateVelVector.X, stateVelVector.Y, stateVelVector.Z);
        return new(position, velocity);
    }

    public Vector3 GetPosition()
    {
        return new Vector3(statePosVector.X, statePosVector.Y, statePosVector.Z);
    }

    public Vector3 GetVelocity()
    {
        return new Vector3(stateVelVector.X, stateVelVector.Y, stateVelVector.Z);
    }
}
