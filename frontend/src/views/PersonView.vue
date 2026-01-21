<template>
    <PersonForm :person="selectedPerson" @saved="loadPersons" />

    <PersonList :persons="persons" @edit-person="selectPerson" />
</template>

<script>
export default {
    data() {
        return {
            persons: [],
            selectedPerson: null
        };
    },
    methods: {
        selectPerson(person) {
            this.selectedPerson = { ...person }; // clone
        },

        async loadPersons() {
            const res = await api.get('/persons');
            this.persons = res.data;
            this.selectedPerson = null;
        }
    }
}
</script>