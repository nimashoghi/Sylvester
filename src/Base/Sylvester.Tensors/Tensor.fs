﻿namespace Sylvester.Tensors

open Sylvester.Arithmetic
open Sylvester.Arithmetic.N10
open Sylvester.Arithmetic.Collections
open System.Collections.Generic

type TensorProps = Dims | DExpr

type Tensor<'n, 't when 'n: (static member Zero : NZero) 
                    and 'n : (static member op_Explicit: 'n -> int) 
                    and 't : (static member Zero: 't) 
                    and 't : (static member (+): 't -> 't -> 't)> () =     
    
    member inline x.Props = 
        let props = Dictionary<TensorProps, Option<obj>>()
        props.Add(Dims, None)
        props.Add(DExpr, None)
        props
    
    member inline x.SetProp(p, v) = x.Props.[p] <- v

    member inline x.Rank = getN<'n>()

    





    




