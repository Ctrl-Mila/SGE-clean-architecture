namespace SGE.Infraestructura.Comun;

public class RepositorioException : Exception
{
    public RepositorioException () { }

    public RepositorioException (string message) : base(message) { }

    public RepositorioException (string message, Exception e) : base(message, e) { }
    
}