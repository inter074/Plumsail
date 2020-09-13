<template>
    <div v-if="currentForm && updated === false" class="edit-form">
        <h4>Form</h4>
        <div class="border">
            <div class="container-fluid">
                <form>
                    <div class="form-group">
                        <div class="row">
                            <div class="form-group col-md-8">
                                <label for="title">Title</label>
                                <input
                                        type="text"
                                        class="form-control"
                                        id="title"
                                        v-model="currentForm.title"
                                />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div  v-for="(element, index) in elements" class="form-group" >
                            <div class="row">
                                <div class="form-group col-md-6">
                                    <FormElement :type = element.type :elementState = element.state  v-on:changeStateOfElement="onChangeState($event, index)"></FormElement>
                                </div>
                                <div class="form-group col-md-6">
                                    <button type="button" v-on:click="deleteFormElement(index)" class="btn btn-danger btn-sm rounded-0">x</button>
                                </div>
                            </div>
                        </div >
                    </div>

                    <div class="form-group">
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
                </form>
            </div>

        </div>
        <div class="btn-group">
            <button type="submit" class="btn btn-success" @click="updateForm">
                Update
            </button>
            <button class="btn btn-danger" @click="deleteForm">
                Delete
            </button>
        </div>
    </div>
    <div v-else-if="message!==''" class="alert alert-success alert-dismissible" role="alert">
        <button type="button" class="close" data-dismiss="alert" aria-label="Close" @click="cleanMessage">
            <span aria-hidden="true">&times;</span></button>
        {{ message }}
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from "vue-property-decorator";
    import FormElement from "@/components/FormElement.vue";
    import FormDataService from "../services/FormDataService";
    @Component({
        components:{
            FormElement
        }
    })
    export default class Form extends Vue {
        private form: any = {
            title: "",
            elements: []
        };
        private selected: string = "";
        private currentForm: any = null;
        private message: string = "";
        private elements: any[] = []
        private updated: boolean = false
        getForm(id: string) {
            FormDataService.get(id)
                .then((response) => {
                    this.currentForm = response.data;
                    this.elements = response.data.elements || []
                    console.log(response.data);
                })
                .catch((e) => {
                    console.log(e);
                });
        }
        updateForm() {
            this.elements.map(element => {
                element.orderIndex = this.elements.indexOf(element)
                this.form.elements.push(element);
            })
            this.form.title = this.currentForm.title || ""
            FormDataService.update(this.currentForm.id, this.form)
                .then((response) => {
                    this.currentForm = response.data;
                    this.elements = response.data.elements || []
                    this.message = "The form was updated successfully!";
                    this.updated = true
                    this.form.elements = []
                })
                .catch((e) => {
                    console.log(e);
                });
        }
        addToForm(type:string, val:any){
            this.elements.push({type:type, state:{value:val}})
        }
        deleteForm() {
            FormDataService.delete(this.currentForm.id)
                .then((response) => {
                    this.$router.push({ name: "forms" });
                })
                .catch((e) => {
                    console.log(e);
                });
        }
        deleteFormElement(index:number){
            this.elements.splice(index, 1)
        }
        onChangeState (value:any, index:number) {
            this.elements[index].state.value = value
        }
        cleanMessage(){
            this.message = "";
            this.updated = false
        }
        mounted() {
            this.message = "";
            this.getForm(this.$route.params.id);
        }
    }
</script>

<style scoped>
    .edit-form {
        max-width: 400px;
        margin: auto;
    }
</style>