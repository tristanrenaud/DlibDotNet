﻿using System;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void GaussianBlur(Array2DBase inImage, Array2DBase outImage, double sigma = 1, int maxSize = 1001)
        {
            if (inImage == null)
                throw new ArgumentNullException(nameof(inImage));
            if (outImage == null)
                throw new ArgumentNullException(nameof(outImage));
            if (!(sigma > 0))
                throw new ArgumentOutOfRangeException(nameof(sigma));
            if (!(0 < maxSize && maxSize % 2 != 0))
                throw new ArgumentOutOfRangeException(nameof(maxSize));

            inImage.ThrowIfDisposed(nameof(inImage));
            outImage.ThrowIfDisposed(nameof(outImage));

            var inType = inImage.ImageType.ToNativeArray2DType();
            var outType = outImage.ImageType.ToNativeArray2DType();
            var ret = Native.gaussian_blur(inType, inImage.NativePtr, outType, outImage.NativePtr, sigma, maxSize);
            switch (ret)
            {
                case Native.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException("Output or input type is not supported.");
            }
        }

        public static void SumFilter(Array2DBase inImage, Array2DBase outImage, Rectangle rect)
        {
            if (inImage == null)
                throw new ArgumentNullException(nameof(inImage));
            if (outImage == null)
                throw new ArgumentNullException(nameof(outImage));

            inImage.ThrowIfDisposed(nameof(inImage));
            outImage.ThrowIfDisposed(nameof(outImage));

            if (inImage.Rows != outImage.Rows || inImage.Columns != outImage.Columns)
                throw new ArgumentException();

            var inType = inImage.ImageType.ToNativeArray2DType();
            var outType = outImage.ImageType.ToNativeArray2DType();
            using (var native = rect.ToNative())
            {
                var ret = Native.sum_filter(inType, inImage.NativePtr, outType, outImage.NativePtr, native.NativePtr);
                switch (ret)
                {
                    case Native.ErrorType.Array2DTypeTypeNotSupport:
                        throw new ArgumentException("Output or input type is not supported.");
                }
            }
        }

        #endregion

        internal sealed partial class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType gaussian_blur(Array2DType inType, IntPtr inImg, Array2DType outtype, IntPtr outImg, double sigma, int maxSize);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType sum_filter(Array2DType inType, IntPtr inImg, Array2DType outtype, IntPtr outImg, IntPtr rect);

        }

    }

}