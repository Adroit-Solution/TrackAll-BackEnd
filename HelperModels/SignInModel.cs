namespace TrackAll_Backend.HelperModels
{
    public class SignInModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsPersistent { get; set; }
    }
}
