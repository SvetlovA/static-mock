using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace StaticMock.Helpers
{
    internal class CodeInjectionHelper
    {
        public static void Inject(MethodInfo methodToReplace, MethodInfo methodToInject)
        {
            if (methodToReplace == null)
            {
                throw new ArgumentNullException(nameof(methodToReplace));
            }

            if (methodToInject == null)
            {
                throw new ArgumentNullException(nameof(methodToInject));
            }

            RuntimeHelpers.PrepareMethod(methodToReplace.MethodHandle);
            RuntimeHelpers.PrepareMethod(methodToInject.MethodHandle);

            unsafe
            {
                if (IntPtr.Size == 4)
                {
                    var inj = (int*)methodToInject.MethodHandle.Value.ToPointer() + 2;
                    var tar = (int*)methodToReplace.MethodHandle.Value.ToPointer() + 2;
#if DEBUG
                    Console.WriteLine("\nVersion x86 Debug\n");

                    var injInst = (byte*)*inj;
                    var tarInst = (byte*)*tar;

                    var injSrc = (int*)(injInst + 1);
                    var tarSrc = (int*)(tarInst + 1);

                    *tarSrc = (((int)injInst + 5) + *injSrc) - ((int)tarInst + 5);
#else
                    Console.WriteLine("\nVersion x86 Release\n");
                    *tar = *inj;
#endif
                }
                else
                {
                    var inj = (long*)methodToInject.MethodHandle.Value.ToPointer() + 1;
                    var tar = (long*)methodToReplace.MethodHandle.Value.ToPointer() + 1;
#if DEBUG
                    Console.WriteLine("\nVersion x64 Debug\n");
                    var injInst = (byte*)*inj;
                    var tarInst = (byte*)*tar;

                    var injSrc = (int*)(injInst + 1);
                    var tarSrc = (int*)(tarInst + 1);

                    *tarSrc = (((int)injInst + 5) + *injSrc) - ((int)tarInst + 5);
#else
                    Console.WriteLine("\nVersion x64 Release\n");
                    *tar = *inj;
#endif
                }
            }
        }
    }
}
