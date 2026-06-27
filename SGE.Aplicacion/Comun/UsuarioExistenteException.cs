namespace SGE.Aplicacion.Comun;

public class UsuarioExistenteException : Exception
{
    public UsuarioExistenteException () {}

    public UsuarioExistenteException (string message) : base (message) {}

    public UsuarioExistenteException (string message, Exception e) : base (message, e) {}
    
}