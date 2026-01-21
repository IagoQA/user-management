<template>
    <div>
        <h2>Lista de Pessoas</h2>

        <input v-model="search" placeholder='Buscar por "Nome" ou "Telefone"' />

        <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nome</th>
                    <th>Telefone</th>
                    <th>Email</th>
                    <th>Endereço</th>
                    <th class="actions">Ações</th>
                </tr>
            </thead>

            <tbody>
                <tr v-for="p in filteredPersons" :key="p.id">
                    <td>{{ p.id }}</td>
                    <td>{{ p.name }}</td>
                    <td>{{ p.phone }}</td>
                    <td>{{ p.email }}</td>
                    <td>{{ p.address }}</td>
                    <td class="actions">
                        <button @click="edit(p)" title="Editar" class="icon-btn">
                            <i class="bi bi-pencil"></i>
                        </button>

                        <button @click="remove(p.id)" title="Excluir" class="icon-btn danger">
                            <i class="bi bi-trash"></i>
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import api from '@/api/api'

export default {
    data() {
        return {
            persons: [],
            search: ''
        }
    },
    computed: {
        filteredPersons() {
            const term = this.normalize(this.search);

            return this.persons.filter(p => {
                return (
                    this.normalize(p.name).includes(term) ||
                    this.normalize(p.phone).includes(term)
                );
            });
        }
    }
    ,
    async mounted() {
        const response = await api.get('/persons')
        this.persons = response.data
    },
    methods: {
        async remove(id) {
            await api.delete(`/persons/${id}`)
            this.persons = this.persons.filter(p => p.id !== id)
        },
        edit(person) {
            this.$emit('edit-person', person)
        },
        normalize(text) {
            return text
                ? text
                    .toString()
                    .normalize('NFD')
                    .replace(/[\u0300-\u036f]/g, '')
                    .toLowerCase()
                : '';
        }
    }
}
</script>

<style>
::placeholder {
    color: #aaa;
}

table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 20px;
}

th,
td {
    text-align: left;
    padding: 8px;
    border-bottom: 2px solid #444;
}

input {
    margin-bottom: 15px;
    padding: 8px;
    width: 300px;
}

.icon-btn {
    background: none;
    border: none;
    cursor: pointer;
    font-size: 14px;
    margin-left: 20px;
}

.icon-btn:hover {
    color: #007bff;
    background-color: transparent;
}

.icon-btn.danger:hover {
    color: #dc3545;
}

.actions {
    text-align: right;
    white-space: nowrap;
}
</style>