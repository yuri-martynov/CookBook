namespace Products

open Types
open Operators

[<AutoOpen>]
module GlobalCereals =
    let рис' = Whole "рис"
    let рис gl = рис' $ Glasses gl