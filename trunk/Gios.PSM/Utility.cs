//============================================================================
//Gios Pdf.NET - A library for exporting Pdf Documents in C#
//Copyright (C) 2005  Paolo Gios - www.paologios.com
//
//This library is free software; you can redistribute it and/or
//modify it under the terms of the GNU Lesser General Public
//License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.
//
//This library is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//Lesser General internal License for more details.
//
//You should have received a copy of the GNU Lesser General Public
//License along with this library; if not, write to the Free Software
//Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//=============================================================================
using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Text;
using System.Collections.Generic;

namespace Gios.PDF.SplitMerge
{
	internal class Utility
	{				
		
		
        internal static int Write(Stream s, string text, bool carriageReturn)
        {
            byte[] buffer = ASCIIEncoding.ASCII.GetBytes(
                carriageReturn ? text + Environment.NewLine : text);
            s.Write(buffer, 0, buffer.Length);
            return buffer.Length;
        }



    }
}
