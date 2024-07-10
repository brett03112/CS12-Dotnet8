namespace Packt.Shared;

public interface IPlayable
{
    void Play();
    void Pause();
    void Stop() // Default interface implementation.  Don't need to implement. ***not recommended***
    {
        WriteLine("Default implementation of Stop.");
    }
}