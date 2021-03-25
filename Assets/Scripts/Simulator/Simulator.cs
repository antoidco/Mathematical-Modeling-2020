using AircraftSimulator.Physics;
using AircraftSimulator.Physics.DariaKlochko;
using AircraftSimulator.Physics.Basic;
using UnityEngine;
using AircraftSimulator.Physics.IlyaAntonov;
using AircraftSimulator.Physics.IgorGruzdev;

namespace AircraftSimulator
{
    // todo: refactor this
    public class Simulator
    {
        public const float GravityConstant = -9.81f;
        private readonly Aircraft _aircraft;

        private PhysicsModel _physicsModel;
        private double _globalTime;
        private Weather _weather;
        public float Time { get; private set; }
        public Simulator(Aircraft aircraft, Weather weather, ModelEnum modelEnum) {
            Time = 0;
            _aircraft = aircraft;
            _weather = weather;

            switch (modelEnum)
            {
                case ModelEnum.IgorGruzdev:
                    _physicsModel = new IgorGruzdevModel(_aircraft, Vector3.zero, _weather, 0.2f,
                new IgorGruzdevModelData
                {
                    ControlRate = 10f,
                    DeadZone = 0.2f,
                    Lerp = 0.03f,
                    MaxTurn = 15.0f,
                    AileronTurnRate = 300f,
                    ElevatorTurnRate = 3f,
                    RudderTurnRate = 100f,
                });
                    break;
                case ModelEnum.Basic:
                    _physicsModel = new BasicPhysicsModel(_aircraft, Vector3.zero, new BasicPhysicsModelData());
                    break;
                case ModelEnum.IlyaAntonov:
                    _physicsModel = new IlyaAntonovModel(_aircraft, Vector3.zero);
                    break;
                case ModelEnum.FedorKondratenko:
                    _physicsModel = new CopterPhysicsModel(_aircraft, Vector3.zero,
                        new CopterPhysicsModelData
                        {
                            ControlRate = 10f,
                            DeadZone = 0.2f,
                            Lerp = 0.03f,
                            MaxTurn = 15.0f,
                            AileronTurnRate = 300f,
                            ElevatorTurnRate = 3f,
                            RudderTurnRate = 100f
                        });
                    break;
                case ModelEnum.DariaKlochko:
                    var forsage = GameObject.FindObjectOfType<Forsage>();
                    _physicsModel = new DariaKlochkoModel(_aircraft, Vector3.zero,
                        new DariaKlochkoModelData
                        {
                            ControlRate = 10f,
                            DeadZone = 0.2f,
                            Lerp = 0.03f,
                            MaxTurn = 15.0f,
                            AileronTurnRate = 300f,
                            ElevatorTurnRate = 3f,
                            Forsage = forsage,
                            RudderTurnRate = 100f,
                        });
                    break;
				case ModelEnum.MaksimVolgin:
                    _physicsModel = new MaksimVolginModel(_aircraft, Vector3.zero,
                    new MaksimVolginModelData
                    {
                        ControlRate = 10f,
                        DeadZone = 0.2f,
                        Lerp = 0.03f,
                        MaxTurn = 15.0f,
                        AileronTurnRate = 300f,
                        ElevatorTurnRate = 3f,
                        RudderTurnRate = 100f,

                        aerodynamicDrag = 0.001f,
                        frontalResistanceArea = 1f,
                        airDensity = 1.2754f,
                    }) ;
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }

        public void Restart(float height)
        {
            _aircraft.Position = new Vector3(0, 0, height);
            _physicsModel.Restart(Vector3.zero);
        }

        public void Update(float timeStep, ControlData controlData) {
            Time += timeStep;
            
            _physicsModel.Update(controlData, timeStep);
        }
    }
}