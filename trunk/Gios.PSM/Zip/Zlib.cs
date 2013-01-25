// Copyright (c) 2009, Dino Chiesa.  
// This code is licensed under the Microsoft internal license.  See the license.txt file in the source
// distribution for details. 
//
// The zlib code is derived from the jzlib implementation, but significantly modified.
// The object model is not the same, and many of the behaviors are different.
// Nonetheless, in keeping with the license for jzlib, I am reproducing the copyright to that code here.
// 
// -----------------------------------------------------------------------
// Copyright (c) 2000,2001,2002,2003 ymnk, JCraft,Inc. All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
// 
// 1. Redistributions of source code must retain the above copyright notice,
// this list of conditions and the following disclaimer.
// 
// 2. Redistributions in binary form must reproduce the above copyright 
// notice, this list of conditions and the following disclaimer in 
// the documentation and/or other materials provided with the distribution.
// 
// 3. The names of the authors may not be used to endorse or promote products
// derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED ``AS IS'' AND ANY EXPRESSED OR IMPLIED WARRANTIES,
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL JCRAFT,
// INC. OR ANY CONTRIBUTORS TO THIS SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA,
// OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
// LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
// EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// 

/*
* This program is based on zlib-1.1.3; credit to authors
* Jean-loup Gailly(jloup@gzip.org) and Mark Adler(madler@alumni.caltech.edu)
* and contributors of zlib.
*/


using System;
namespace Ionic.Zlib
{

    /// <summary>
    /// The compression level to be used when using a DeflateStream or ZlibStream with CompressionMode.Compress.
    /// </summary>
    internal enum CompressionLevel
    {
        /// <summary>
        /// NONE means that the data will be simply stored, with no change at all.
        /// </summary>
        NONE = 0,
        /// <summary>
        /// Same as NONE.
        /// </summary>
        LEVEL0_NONE = 0,

        /// <summary>
        /// The fastest but least effective compression.
        /// </summary>
        BEST_SPEED = 1,
        /// <summary>
        /// A synonym for BEST_SPEED.
        /// </summary>
        LEVEL1_BEST_SPEED = 1,

        /// <summary>
        /// A little slower, but better, than level 1.
        /// </summary>
        LEVEL2 = 2,
        /// <summary>
        /// A little slower, but better, than level 2.
        /// </summary>
        LEVEL3 = 3,

        /// <summary>
        /// A little slower, but better, than level 3.
        /// </summary>
        LEVEL4 = 4,
        /// <summary>
        /// A little slower, but better, than level 4.
        /// </summary>
        LEVEL5 = 5,

        /// <summary>
        /// The default compression level.  Do these levels even matter?  Do people even care?  
        /// I have never measured the speed difference.  
        /// </summary>
        DEFAULT = 6,
        /// <summary>
        /// A synonym for DEFAULT.
        /// </summary>
        LEVEL6_DEFAULT = 6,

        /// <summary>
        /// Pretty good compression!
        /// </summary>
        LEVEL7 = 7,

        /// <summary>
        ///  Still better compression!
        /// </summary>
        LEVEL8 = 8,

        /// <summary>
        /// The "best" compression, where best means greatest reduction in size of the input data stream. 
        /// This is also the slowest compression.
        /// </summary>
        BEST_COMPRESSION = 9,

        /// <summary>
        /// A synonym for BEST_COMPRESSION.
        /// </summary>
        LEVEL9_BEST_COMPRESSION = 9,
    }

  

    /// <summary>
    /// An enum to specify the direction of transcoding - whether to compress or decompress.
    /// </summary>
    internal enum CompressionMode
    {
        /// <summary>
        /// Used to specify that the stream should compress the data.
        /// </summary>
        Compress= 0,
        /// <summary>
        /// Used to specify that the stream should decompress the data.
        /// </summary>
        Decompress = 1,
    }
       
    
    /// <summary>
    /// A general purpose exception class for exceptions in the Zlib library.
    /// </summary>
    internal class ZlibException : System.Exception
    {
        /// <summary>
        /// The ZlibException class captures exception information generated
        /// by the Zlib library. 
        /// </summary>
        internal ZlibException()
            : base()
        {
        }

        /// <summary>
        /// This ctor collects a message attached to the exception.
        /// </summary>
        /// <param name="s"></param>
        internal ZlibException(System.String s)
            : base(s)
        {
        }
    }


    internal class SharedUtils
    {
        /// <summary>
        /// Performs an unsigned bitwise right shift with the specified number
        /// </summary>
        /// <param name="number">Number to operate on</param>
        /// <param name="bits">Ammount of bits to shift</param>
        /// <returns>The resulting number from the shift operation</returns>
        internal static int URShift(int number, int bits)
        {
            if (number >= 0)
                return number >> bits;
            else
                return (number >> bits) + (2 << ~bits);
        }

        /// <summary>
        /// Performs an unsigned bitwise right shift with the specified number
        /// </summary>
        /// <param name="number">Number to operate on</param>
        /// <param name="bits">Ammount of bits to shift</param>
        /// <returns>The resulting number from the shift operation</returns>
        internal static int URShift(int number, long bits)
        {
            return URShift(number, (int)bits);
        }

        /// <summary>
        /// Performs an unsigned bitwise right shift with the specified number
        /// </summary>
        /// <param name="number">Number to operate on</param>
        /// <param name="bits">Ammount of bits to shift</param>
        /// <returns>The resulting number from the shift operation</returns>
        internal static long URShift(long number, int bits)
        {
            if (number >= 0)
                return number >> bits;
            else
                return (number >> bits) + (2L << ~bits);
        }

        /// <summary>
        /// Performs an unsigned bitwise right shift with the specified number
        /// </summary>
        /// <param name="number">Number to operate on</param>
        /// <param name="bits">Ammount of bits to shift</param>
        /// <returns>The resulting number from the shift operation</returns>
        internal static long URShift(long number, long bits)
        {
            return URShift(number, (int)bits);
        }

        /*******************************/
        /// <summary>Reads a number of characters from the current source Stream and writes the data to the target array at the specified index.</summary>
        /// <param name="sourceStream">The source Stream to read from.</param>
        /// <param name="target">Contains the array of characteres read from the source Stream.</param>
        /// <param name="start">The starting index of the target array.</param>
        /// <param name="count">The maximum number of characters to read from the source Stream.</param>
        /// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source Stream. Returns -1 if the end of the stream is reached.</returns>
        internal static System.Int32 ReadInput(System.IO.Stream sourceStream, byte[] target, int start, int count)
        {
            // Returns 0 bytes if not enough space in target
            if (target.Length == 0)
                return 0;

            if (count == 0) return 0;

            // why double-buffer?
            //byte[] receiver = new byte[target.Length];
            int bytesRead = sourceStream.Read(target, start, count);

            // Returns -1 if EOF
            //if (bytesRead == 0)
            //    return -1;

            //for (int i = start; i < start + bytesRead; i++)
            //    target[i] = (byte)receiver[i];

            return bytesRead;
        }

        /// <summary>Reads a number of characters from the current source TextReader and writes the data to the target array at the specified index.</summary>
        /// <param name="sourceTextReader">The source TextReader to read from</param>
        /// <param name="target">Contains the array of characteres read from the source TextReader.</param>
        /// <param name="start">The starting index of the target array.</param>
        /// <param name="count">The maximum number of characters to read from the source TextReader.</param>
        /// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source TextReader. Returns -1 if the end of the stream is reached.</returns>
        internal static System.Int32 ReadInput(System.IO.TextReader sourceTextReader, byte[] target, int start, int count)
        {
            // Returns 0 bytes if not enough space in target
            if (target.Length == 0) return 0;

            char[] charArray = new char[target.Length];
            int bytesRead = sourceTextReader.Read(charArray, start, count);

            // Returns -1 if EOF
            if (bytesRead == 0) return -1;

            for (int index = start; index < start + bytesRead; index++)
                target[index] = (byte)charArray[index];

            return bytesRead;
        }


        internal static byte[] ToByteArray(System.String sourceString)
        {
            return System.Text.UTF8Encoding.UTF8.GetBytes(sourceString);
        }


        internal static char[] ToCharArray(byte[] byteArray)
        {
            return System.Text.UTF8Encoding.UTF8.GetChars(byteArray);
        }

    }
    

    /// <summary>
    /// Computes an Adler-32 checksum. 
    /// </summary>
    /// <remarks>
    /// The Adler checksum is similar to a CRC checksum, but faster to compute, though less reliable.  
    /// It is used in producing RFC1950 compressed streams.  The Adler checksum is a required part of the "ZLIB" standard.
    /// Applications will almost never need to use this class directly. 
    /// </remarks>
    internal sealed class Adler
    {
        // largest prime smaller than 65536
        private static int BASE = 65521;
        // NMAX is the largest n such that 255n(n+1)/2 + (n+1)(BASE-1) <= 2^32-1
        private static int NMAX = 5552;

        static internal long Adler32(long adler, byte[] buf, int index, int len)
        {
            if (buf == null)
            {
                return 1L;
            }

            long s1 = adler & 0xffff;
            long s2 = (adler >> 16) & 0xffff;
            int k;

            while (len > 0)
            {
                k = len < NMAX ? len : NMAX;
                len -= k;
                while (k >= 16)
                {
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    s1 += (buf[index++] & 0xff); s2 += s1;
                    k -= 16;
                }
                if (k != 0)
                {
                    do
                    {
                        s1 += (buf[index++] & 0xff); s2 += s1;
                    }
                    while (--k != 0);
                }
                s1 %= BASE;
                s2 %= BASE;
            }
            return (s2 << 16) | s1;
        }

        /*
        private java.util.zip.Adler32 adler=new java.util.zip.Adler32();
        long adler32(long value, byte[] buf, int index, int len){
        if(value==1) {adler.reset();}
        if(buf==null) {adler.reset();}
        else{adler.update(buf, index, len);}
        return adler.getValue();
        }
        */
    }

}