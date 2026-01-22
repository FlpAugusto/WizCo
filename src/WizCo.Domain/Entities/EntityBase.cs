namespace WizCo.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool Visible { get; protected set; } = true;

        public void Delete()
        {
            Visible = false;
        }
    }
}
