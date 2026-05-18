public class EntidadNoEncontradaException : Exception
{
    public EntidadNoEncontradaException () {}

    public EntidadNoEncontradaException (string message) : base(message) {}

    public EntidadNoEncontradaException (string message, Exception e) : base(message, e) {}
    
}