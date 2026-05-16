using SGE.Dominio.Comun;

namespace SGE.Dominio.Expedientes;

public record class Caratula
{
    public string NombreCaratula { get; }

    public Caratula (string c)
    {
        if (string.IsNullOrWhiteSpace(c))
        {
            throw new DominioException ("La carátula no puede estar vacía");
        }
        NombreCaratula = c.Trim();
    }

    public override string ToString() => NombreCaratula;
}    
