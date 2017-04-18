namespace Products

open Types
open Operators

[<AutoOpen>]
module GlobalFruits =
    let гранат = Whole "гранат"

    let ``зёрна граната'`` = "зёрна" @ гранат
    let ``зёрна граната`` items = ``зёрна граната'`` $ Items items