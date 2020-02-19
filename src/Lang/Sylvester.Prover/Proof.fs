﻿namespace Sylvester

open Microsoft.FSharp.Quotations

type Axioms = (Expr * Expr -> bool)

type Rule = Rule of string * (Expr * Expr -> Expr * Expr) with
    member x.Name = let (Rule(n, _)) = x in n
    member x.Apply = let (Rule(_, r)) = x in r
    
type Rules = Rule list 

type ProofSystem(axioms: Axioms, rules: Rules) =
    member val Axioms = axioms
    member val Rules = rules
    member x.AxiomaticallyEquivalent a b = x.Axioms (a, b)     
    static member (|-) ((c:ProofSystem), (a, b)) = c.AxiomaticallyEquivalent a b 

type Proof(system: ProofSystem, a:Expr,  b:Expr,  steps: Rule list) =
    let ruleNames = system.Rules |> List.map (fun r -> r.Name)
    let stepNames = steps |> List.map (fun r -> r.Name)
    do printfn "Proof of A:%s <=> B:%s." (decompile a) (decompile b)
    let mutable astate, bstate = (a, b)
    let mutable stepCount = 0
    do while stepCount < steps.Length do
        if not(List.contains stepNames.[stepCount] ruleNames) then
            failwithf "Step %i (%s) is not part of the rules of the proof system." stepCount stepNames.[stepCount]
        else
            let (_a, _b) = steps.[stepCount].Apply (astate, bstate)
            if (not (sequal _a astate) && sequal _b bstate) then
                printfn "%i. %s: (%s, %s) -> (%s, %s)" (stepCount + 1) stepNames.[stepCount] (decompile astate) (decompile bstate) (decompile _a) (decompile _b)
            else if not (sequal _a astate) then
                printfn "%i. %s: %s -> %s" (stepCount + 1) stepNames.[stepCount] (decompile astate) (decompile _a)
            else if not (sequal _b bstate) then
                printfn "%i. %s: %s -> %s" (stepCount + 1) stepNames.[stepCount] (decompile bstate) (decompile _b)
            else
                printfn "%i. %s: No change." (stepCount + 1) stepNames.[stepCount] 
            astate <- _a
            bstate <- _b
            if system |- (astate, bstate) then 
                printfn "Proof complete." 
                stepCount <- steps.Length
            else
                printfn "Proof incomplete."
                stepCount <- stepCount + 1
    
    member val Complete = system |- (astate, bstate)
    member val A = a
    member val B = b
    member val System = system
    member val Steps = steps
    static member (|-) ((proof:Proof), (a, b)) = proof.A = a && proof.B = b && proof.Complete
    static member (|-) ((proof:Proof), (A:Formula<'t,'u>, B:Formula<'v,'w>)) = proof.A = A.Expr && proof.B = B.Expr && proof.Complete

type Theorem<'t, 'u>(stms:TheoremStmt<'t, 'u>, proof:Proof) = 
    let (a, b) = stms
    do if not ((sequal proof.A a.Expr) && (sequal proof.B b.Expr)) then failwithf "The provided proof is not a proof of %s<=>%s" (a.Src) (b.Src)
    do if not (proof.Complete) then failwithf "The provided proof of %s<=>%s is not complete." (a.Src) (b.Src)
    member val A = a
    member val B = b
    member val Proof = proof
 and TheoremStmt<'t, 'u> = Formula<'t, 'u> * Formula<'t, 'u>

module Proof =   
    let thm (stmt:TheoremStmt<_,_>) (proof) = Theorem(stmt, proof)