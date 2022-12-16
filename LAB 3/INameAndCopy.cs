namespace LAB_3
{
    interface INameAndCopy
    {
        string Name { set; get; }
        object DeepCopy();
    }
}
