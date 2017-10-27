using Castle.Windsor;

namespace GlobalContainer
{
    public class Container
    {
        public static WindsorContainer Instance { get; private set; }

        public static void Initialize(WindsorContainer container)
        {
            if (Instance == null)
            {
                Instance = container;
            }
        }
    }
}
