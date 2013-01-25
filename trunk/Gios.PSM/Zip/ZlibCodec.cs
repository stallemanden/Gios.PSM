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
    /// Encoder and Decoder for ZLIB (IETF RFC1950 and RFC1951).
    /// </summary>
    /// <remarks>
    /// This class compresses and decompresses data according to the Deflate algorithm documented in RFC1950 and RFC1951. 
    /// </remarks>
    sealed internal class ZlibCodec
    {        
        internal byte[] InputBuffer;

        /// <summary>
        /// An index into the InputBuffer array, indicating where to start reading. 
        /// </summary>
        internal int NextIn;

        
        internal int AvailableBytesIn;

        /// <summary>
        /// Total number of bytes read so far, through all calls to Inflate()/Deflate().
        /// </summary>
        internal long TotalBytesIn;

       
        internal byte[] OutputBuffer;

        
        internal int NextOut;

      
        internal int AvailableBytesOut;

        /// <summary>
        /// Total number of bytes written to the output so far, through all calls to Inflate()/Deflate().
        /// </summary>
        internal long TotalBytesOut;

        /// <summary>
        /// used for diagnostics, when something goes wrong!
        /// </summary>
        internal System.String Message;

        //internal DeflateManager dstate;
        internal InflateManager istate;

        internal long _Adler32;

        /// <summary>
        /// The Adler32 checksum on the data transferred through the codec so far. You probably don't need to look at this.
        /// </summary>
        internal long Adler32 { get { return _Adler32; } }
               
        internal ZlibCodec() { }

     
        internal ZlibCodec(CompressionMode mode)
        {
            if (mode == CompressionMode.Compress)
            {
                throw new NotImplementedException();
                //int rc = InitializeDeflate();
                //if (rc != ZlibConstants.Z_OK) throw new ZlibException("Cannot initialize for deflate.");
            }
            else if (mode == CompressionMode.Decompress)
            {
                int rc = InitializeInflate();
                if (rc != ZlibConstants.Z_OK) throw new ZlibException("Cannot initialize for inflate.");
            }
            else throw new ZlibException("Invalid ZlibStreamFlavor.");
        }

        internal int InitializeInflate()
        {
            return InitializeInflate(ZlibConstants.WINDOW_BITS_DEFAULT);
        }

        internal int InitializeInflate(bool expectRfc1950Header)
        {
            return InitializeInflate(ZlibConstants.WINDOW_BITS_DEFAULT, expectRfc1950Header);
        }

        internal int InitializeInflate(int windowBits)
        {
            return InitializeInflate(windowBits, true);
        }

        internal int InitializeInflate(int windowBits, bool expectRfc1950Header)
        {
            //if (dstate != null) throw new ZlibException("You may not call InitializeInflate() after calling InitializeDeflate().");
            istate = new InflateManager(expectRfc1950Header);
            return istate.Initialize(this, windowBits);
        }

        internal int Inflate(int f)
        {
            if (istate == null)
                throw new ZlibException("No Inflate State!");
            return istate.Inflate(this, f);
        }

        internal int EndInflate()
        {
            if (istate == null)
                throw new ZlibException("No Inflate State!");
            int ret = istate.End(this);
            istate = null;
            return ret;
        }

        internal int SyncInflate()
        {
            if (istate == null)
                throw new ZlibException("No Inflate State!");
            return istate.Sync(this);
        }

    }
}