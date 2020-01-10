﻿namespace Sylvester.tf

open System

open Sylvester.Arithmetic.N10
open Sylvester.Tensors

[<AutoOpen>]
module Scalar =

    [<StructuredFormatDisplay("<Scalar>")>]
    type Scalar<'t when 't: struct and 't:> ValueType and 't: (new: unit -> 't) and 't :> IEquatable<'t> and 't :> IFormattable and 't :> IComparable>
        internal (graph:ITensorGraph, head:Node, output:int) = 
        inherit Tensor<zero, 't>(graph, head, output, [|0L|])
        interface IScalar
        
        internal new(name:string, ?graph:ITensorGraph) = 
            let g = defaultArg graph defaultGraph
            let shape = [|0L|]
            let op = tf(g).Placeholder(dataType<'t>, shape, name)
            new Scalar<'t>(g, new Node(g, op, []), 0)