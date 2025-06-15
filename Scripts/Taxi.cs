using UnityEngine;
//INHERITANCE
public class Taxi : Vehicle
{
    protected override void Awake()
    {
        Speed = 10;
        Acceleration = 50;
        AngularSpeed = 300;
        base.Awake();
    }
    // POLYMORPHISM
    public override void GoTo(Stand target) 
    {
        //GotoBus or Taxi Stand
        Stand busStand = target as Stand;
        if (busStand != null)
        {
            base.GoTo(target);
        }
    }
    // POLYMORPHISM
    public override void GoTo(Vector3 position) 
    {
        //only goto Bus or Taxi Station
        //base.GoTo(position);
    }


}
