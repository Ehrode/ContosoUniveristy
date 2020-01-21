namespace ContosoUniveristy.Core.Entities
{
    public abstract class Person : BaseEntity
    {
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public string FullName => LastName + ", " + FirstMidName;
    }
}