namespace Game.Core.Unity
{
    public class CharacterTransform
    {
        public ITransform Body { get; set; }
        public ITransform Barrel { get; set; }

        public CharacterTransform(ITransform body,ITransform barrel)
        {
            Body = body;
            Barrel = barrel;
        }
    }
}