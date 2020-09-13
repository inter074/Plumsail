import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
    {
        path: "/",
        alias: "/forms",
        name: "forms",
        component: () => import("../components/FormList.vue")
    },
    {
        path: "/form/:id",
        name: "form-details",
        component: () => import("../components/Form.vue")
    },
    {
        path: "/add",
        name: "add",
        component: () => import("../components/AddForm.vue")
    }
];

const router = new VueRouter({
    mode: "history",
    base: process.env.BASE_URL,
    routes
});

export default router;