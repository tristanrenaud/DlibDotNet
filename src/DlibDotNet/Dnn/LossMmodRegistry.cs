﻿using System;

namespace DlibDotNet.Dnn
{

    public static class LossMmodRegistry
    {

        #region Methods

        public static bool Add(IntPtr builder)
        {
            return NativeMethods.LossMmodRegistry_add(builder);
        }

        public static void Remove(IntPtr builder)
        {
            NativeMethods.LossMmodRegistry_remove(builder);
        }

        #endregion

    }

}