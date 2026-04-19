namespace IRIS_API.Models { 

    public enum UserRole { Normal, Agente }
    public class User { 
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public UserRole Role { get; set; }

    }


}
