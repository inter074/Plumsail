<template>
    <div v-if="elementState">
        <input :type=this.type class="form-control" @change="emitToParent ($event)" :value="elementState.value" :checked="elementState.value"/>
    </div>
    <div v-else>
        <input :type=this.type class="form-control" @change="emitToParent ($event)" />
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from "vue-property-decorator";

    @Component({
        props: {
            type: String,
            elementState: Object as () => any
        }
    })

    export default class FormElement extends Vue {
        private type: string = this.$props.type;
        private elementState: any =  this.$props.elementState
        private state: any = ""
        emitToParent  (event:any) {
            switch (this.type) {
                case "text":
                    this.state = {value:event.target.value}
                    break;
                case "checkbox":
                    this.state = {value:event.target.checked}
                    break;
                case "date":
                    this.state = {value:event.target.value}
                    break;
                case "radio":
                    this.state = {value:event.target.checked}
                    break;
            }
            this.$emit('changeStateOfElement', this.state.value)
        }
    }
</script>

<style scoped>

</style>