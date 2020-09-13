<template>
    <div id="app">
        <nav class="navbar navbar-expand navbar-dark bg-dark">
            <div class="navbar-nav mr-auto">
                <li class="nav-item">
                    <a href="/" class="nav-link">Forms</a>
                </li>
                <li class="nav-item">
                    <a href="/add" class="nav-link">Create a new form</a>
                </li>
            </div>
        </nav>

        <div class="container mt-3">
            <router-view />
        </div>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from "vue-property-decorator";
    @Component
    export default class App extends Vue {}
</script>