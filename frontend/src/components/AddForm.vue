<template>
    <div class="submit-form">
        <div class="border">
            <div class="container-fluid">
                <form>
                    <div v-if="!submitted">
                        <div class="form-group">
                            <div class="row">
                                <div class="form-group col-md-8">
                                    <label for="title">Title</label>
                                    <input
                                            type="text"
                                            class="form-control"
                                            id="title"
                                            required
                                            v-model="form.title"
                                            name="title"
                                    />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div  v-for="(element, index) in elements" class="form-group" >
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <FormElement :type = element.type v-on:changeStateOfElement="onChangeState($event, index)"></FormElement>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <button type="button" v-on:click="deleteFormElement(index)" class="btn btn-danger btn-sm rounded-0">x</button>
                                    </div>
                                </div>
                            </div >

                            <div class="row">
                                <div class="col">
                                    <select v-model="selected" class="browser-default custom-select">
                                        <option>checkbox</option>
                                        <option>radio</option>
                                        <option>text</option>
                                        <option>date</option>
                                    </select>
                                </div>
                                <div class="col"><button type="button" v-on:click="addToForm(selected)" class="btn btn-light">+</button></div>
                            </div>
                        </div>
                    </div>

                    <div v-else>
                        <h4>Saved successfully</h4>
                        <button class="btn btn-success" @click="newForm">Add</button>
                    </div>
                </form>
            </div>
        </div>
        <button v-if="!submitted" @click="saveForm" class="btn btn-success">Save</button>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from "vue-property-decorator";
    import FormDataService from "../services/FormDataService";
    import FormElement from "@/components/FormElement.vue";
    @Component({
        components:{
            FormElement
        }
    })
    export default class AddForm extends Vue {
        private form: any = {
            title: "",
            elements: []
        };
        private selected: string = "";
        private submitted: boolean = false;
        private elements: any[] = []
        saveForm() {
            this.elements.map(element => {
                element.orderIndex = this.elements.indexOf(element)
                this.form.elements.push(element);
            })
            console.log(this.form.elements)
            FormDataService.create(this.form)
                .then((response) => {
                    this.form.id = response.data.id
                    this.submitted = true;
                })
                .catch((e) => {
                    console.log(e);
                });
        }
        addToForm(type:string, val:any){
            this.elements.push({type:type, state:{value:val}})
        }
        newForm() {
            this.submitted = false;
            this.form = {};
        }
        deleteFormElement(index:number){
            this.elements.splice(index, 1)
        }
        onChangeState (value:any, index:number) {
            if (this.elements[index])
                this.elements[index].state.value = value
        }
    }
</script>

<style scoped>
    .submit-form {
        max-width: 300px;
        margin: auto;
    }
</style>