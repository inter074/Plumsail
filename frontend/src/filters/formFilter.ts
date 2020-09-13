export class FormFilter {
    take:number = 1;
    skip:number = 1;
    title:string = "";
    constructor(take:number, skip:number, title:string = "") {
        this.title = title;
        this.take = take;
        this.skip = skip;
    }
}