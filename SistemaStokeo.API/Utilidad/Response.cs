namespace SistemaStokeo.API.Utilidad
{
    public class Response<T> //la T es como para uso generico
    {
        public bool status { get; set; }
        public T Value { get; set; }
        public string Msg { get; set; }
        public string Token { get; set; }

    }
}