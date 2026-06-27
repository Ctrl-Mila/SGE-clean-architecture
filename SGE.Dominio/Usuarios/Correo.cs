using SGE.Dominio.Comun;

namespace SGE.Dominio.Usuarios;

public record class Correo
{
    public string Cuenta { get; private set; }
    public string Dominio { get; private set; }

    protected Correo () {}

    public Correo (string emailCompleto)
    {
        if (string.IsNullOrWhiteSpace(emailCompleto) || !emailCompleto.Contains('@'))
        {
            throw new DominioException("El formato del email es inválido");
        }

        var partes = emailCompleto.Split('@');
        
        if (partes.Length != 2 || string.IsNullOrWhiteSpace(partes[0]) || string.IsNullOrWhiteSpace(partes[1]))
        {
            throw new DominioException("El formato del email es inválido");
        }

        Cuenta = partes[0];
        Dominio = partes[1];
    }

    public override string ToString()
    {
        return $"{Cuenta}@{Dominio}";
    }
}