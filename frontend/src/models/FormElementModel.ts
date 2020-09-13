
export class FormElementModel  {
    id = undefined;
    type: string;
    value: any;
    constructor(type: string, val:any) {
        this.type = type;
        this.value = val;
    }
}