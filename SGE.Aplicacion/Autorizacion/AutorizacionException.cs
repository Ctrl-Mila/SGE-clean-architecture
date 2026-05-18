namespace SGE.Aplicacion.Autorizacion;
public class AutorizacionException : Exception
{
    public AutorizacionException () {}

    public AutorizacionException (string message) : base(message) {}

    public AutorizacionException (string message, Exception e) : base(message, e) {}
    
}