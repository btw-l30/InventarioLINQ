public class Curso
{
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public int Creditos { get; set; }

    public Curso(string codigo, string nombre, int creditos)
    {
        Codigo = codigo;
        Nombre = nombre;
        Creditos = creditos;
    }

    public override string ToString()
    {
        return $"Código: {Codigo}, Curso: {Nombre}, Créditos: {Creditos}";
    }
}