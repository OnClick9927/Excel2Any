namespace Excel2Any
{
    [Entity(typeof(LuaEntity))]
    public class LuaSaver : TextSaver
    {
        public LuaSaver(ISetting setting) : base(setting)
        {
            extension = "lua";
        }
    }
}


