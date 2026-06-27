namespace SGE.Aplicacion.Comun;

public interface IContraseñaHasher
{
    string CalcularHash (string contraseña);
}