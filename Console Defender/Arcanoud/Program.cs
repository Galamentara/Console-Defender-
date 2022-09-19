using System.Threading;


void frame(ref byte[,] place)
{
    Console.Clear();
    for(byte i = 0; i < place.GetLength(0); i++)
    {
        for (byte j = 0; j < place.GetLength(1); j++)
        {
            Console.Write(place[i, j]);
        }
    }
    Thread.Sleep(1000);
}

byte[,] space = new byte[40, 100];
Console.SetWindowSize(100, 40);
frame(ref space);
frame(ref space);
frame(ref space);





class Ball
{
    public byte[] position = { 50, 20 };

}