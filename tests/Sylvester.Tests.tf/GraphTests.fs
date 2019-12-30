﻿namespace Sylvester.Tests.tf

open System

open Xunit

open Sylvester
open Sylvester.Arithmetic
open Sylvester.Arithmetic.N10
open Sylvester.Collections
open Sylvester.tf
open Sylvester.Tests

open TensorFlow

type GraphTests() =
    inherit BaseTest()

    [<Fact>]
    let ``Can create graph`` () =
        let g = TensorGraph<seven, one>()
        checklt(zero, g.NumInputs)
        Assert.True(g.Initialized)
        Assert.NotNull(g._Graph)
        Assert.Equal((int) g.NumInputs, 7)
        ()

    [<Fact>]
    let ``Can create graph inputs`` () =
        let g = TensorGraph<n<3>, n<1>>()
        checkgt(four, g.NumInputs)
        Assert.True(g.Initialized)
        Assert.NotNull(g._Graph)
        Assert.Equal((int) g.NumInputs, 3)
        let foo = g.ScalarInput<int>("foo")
        //g.Inputs <- varray n<3>.create() [|foo; foo; foo|]
        Assert.Equal("foo", g.Inputs.[zero].Name)
        ()

    [<Fact>]
    let ``Can create default graph inputs`` () =
        let A = new Matrix<five, one, int>("A")
        Assert.Equal("A", A.Name)
        Assert.Equal("_", A.Graph.NameScope)
        


        
