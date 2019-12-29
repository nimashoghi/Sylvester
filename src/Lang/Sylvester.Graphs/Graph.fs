﻿namespace Sylvester.Graphs

open System
open System.Linq

open Sylvester
open Sylvester.Arithmetic
open Sylvester.Arithmetic.N10
open Sylvester.Collections
open Sylvester.Tensors

[<AbstractClass>]
type Graph<'input, 'output, 'edge when 'input :> Number and 'output :> Number and 'edge :> IEdge>(scope:string) = 
    inherit Api()
    
    member x.NumInputs = number<'input>

    member x.NumOutputs = number<'input>

    member x.Inputs:VArray<'input, 'edge> = VArray<'input, 'edge>()
    
    member x.Outputs:VArray<'output, 'edge> = VArray<'output, 'edge>()
 
    interface IGraph with
        member x.NameScope = scope
        member val Handle = IntPtr.Zero with get 

and IGraph = 
    abstract member NameScope:string
    abstract member Handle:nativeint

and IEdge = 
    inherit IUnknownShape
    abstract member Graph:IGraph
    abstract member Name:string
    abstract member _DataType:int64

and IEdge<'n when 'n :> Number> = 
        inherit IEdge
        inherit IPartialShape<'n>

and IOutputEdge = 
    inherit IUnknownShape
    inherit IEdge

and IOutputEdge<'n when 'n :> Number> = 
    inherit IOutputEdge
    inherit IPartialShape<'n>