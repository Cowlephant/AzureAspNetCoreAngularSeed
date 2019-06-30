import { NgModule } from "@angular/core";

import { CustomMaterialModule } from "./custom-material.module";

@NgModule({
    declarations: [],
    imports: [CustomMaterialModule],
    exports: [CustomMaterialModule],
    providers: [],
    entryComponents: []
})
export class SharedModule { }