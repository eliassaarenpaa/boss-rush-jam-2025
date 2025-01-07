using UnityEngine;
using Cursor = Project.Runtime.Core.Input.Cursor;

namespace Project.Runtime.Core.Entry
{
    public static class Bootstrapper
    {
        public static BootConfig config;

        [RuntimeInitializeOnLoadMethod]
        private static void Boot()
        {
            if (config.isNormalBoot)
            {
                
            } else
            {
                Cursor.Hide();
                Cursor.Lock();
            }
        }
    }
}
