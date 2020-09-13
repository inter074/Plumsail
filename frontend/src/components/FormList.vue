<template>
    <div class="list row">
        <div class="col-md-8">
            <div class="input-group mb-3">
                <input
                        type="text"
                        class="form-control"
                        placeholder="Search by title"
                        v-model="title"
                />
                <div class="input-group-append">
                    <button
                            class="btn btn-outline-secondary"
                            type="button"
                            @click="searchTitle"
                    >
                        Search
                    </button>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <h4>Your list of forms</h4>
            <ul class="list-group">
                <li
                        class="list-group-item"
                        :class="{ active: index == currentIndex }"
                        v-for="(form, index) in forms"
                        :key="index"
                        @click="setActiveForm(form, index)"
                >
                    {{ form.title }}
                </li>
            </ul>

            <button class="m-3 btn btn-sm btn-danger" @click="deleteAll">
                Remove All
            </button>
        </div>
        <div class="col-md-6">
            <div v-if="currentForm">
                <div>
                    <label><strong>Title:</strong></label> {{ currentForm.title }}
                </div>
                <a
                        class="badge badge-warning"
                        :href="'/form/' + currentForm.id"
                >
                    Edit
                </a>
            </div>
            <div v-else-if="forms.length > 0">
                <br />
                <p>Click on a form for edit</p>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from "vue-property-decorator";
    import FormDataService from "../services/FormDataService";
    import {FormFilter} from "../filters/formFilter";
    import FormElement from "../components/FormElement.vue";
    @Component({
        components:{
            FormElement
        }
    })
    export default class FormList extends Vue {
        private forms: any[] = [];
        private currentForm: any = null;
        private currentIndex: number = -1;
        private title: string = "";
        getForms() {
            FormDataService.getByFilter(new FormFilter(20, 0))
                .then((response) => {
                    this.forms = response.data;
                    console.log(response.data);
                })
                .catch((e) => {
                    console.log(e);
                });
        }
        refreshList() {
            this.getForms();
            this.currentForm = null;
            this.currentIndex = -1;
        }
        setActiveForm(form: any, index: number) {
            this.currentForm = form;
            this.currentIndex = index;
        }
        deleteAll() {
            FormDataService.deleteAll()
                .then((response) => {
                    console.log(response.data);
                    this.refreshList();
                })
                .catch((e) => {
                    console.log(e);
                });
        }
        searchTitle() {
            FormDataService.getByFilter(new FormFilter(-1, 0, this.title))
                .then((response) => {
                    this.forms = response.data;
                    console.log(response.data);
                })
                .catch((e) => {
                    console.log(e);
                });
        }
        mounted() {
            this.getForms();
        }
    }
</script>

<style scoped>
    .list {
        text-align: left;
        max-width: 750px;
        margin: auto;
    }
</style>