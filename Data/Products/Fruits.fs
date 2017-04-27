namespace Products

open Types
open Operators

[<AutoOpen>]
module GlobalFruits =
    let гранат = Whole "гранат"

    let ``зёрна граната'`` = "зёрна" @ гранат
    
    let лимон = Whole "лимон"
    let ``цедра лимона`` = "цедра" @ лимон
    let ``сок лимона`` = "сок" @ лимон


    let клубника = Whole "клубника"
    let персик = Whole "персик"
    