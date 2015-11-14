﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PCLCrypto.Formatters;
using Xunit;
using Xunit.Abstractions;

public class AsnTests
{
    private readonly ITestOutputHelper logger;

    public AsnTests(ITestOutputHelper logger)
    {
        this.logger = logger;
    }

    [Fact]
    public void ContentsOfVaryingLengths()
    {
        int[] interestingLengths = new int[] { 0, 1, 0x7f, 0x80, 0x81, 0xfe, 0xff, 0x100, 0x101, 0x2000 };
        foreach (int length in interestingLengths)
        {
            this.logger.WriteLine("Testing length {0}", length);
            byte[] encoded = Asn.WriteAsn1Element(new Asn.DataElement(Asn.BerClass.Application, Asn.BerPC.Constructed, Asn.BerTag.BitString, new byte[length]));
            var element = Asn.ReadAsn1Elements(encoded).Single();
            Assert.Equal(length, element.Content.Length);
        }
    }
}
