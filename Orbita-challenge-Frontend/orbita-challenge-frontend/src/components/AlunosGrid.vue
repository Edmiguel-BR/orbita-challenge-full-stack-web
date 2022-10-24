<template>
    <div>
        <v-divider class="mx-8" inset vertical> </v-divider>
        <v-data-table :headers="headers" :items="alunos" :items-per-page="5" style="width: 100%">
            <template v-slot:[`item.actions`]="{ item }">
                <v-btn class="mx-2" fab dark small @click="showEditDialog(item)" color="teal">
                    <v-icon small>mdi-pencil</v-icon>
                </v-btn>
                <v-btn class="mx-2" fab dark small @click="deleteItem(item)" color="error">
                    <v-icon small>mdi-delete</v-icon>
                </v-btn>
            </template>
        </v-data-table>
        <v-flex xs12 offset-xs11>
            <v-dialog v-model="dialog" max-width="700px">
                <template v-slot:activator="{}">
                    <div class="d-flex">
                        <v-btn right color="primary" class="mx-2" fab dark small @click="showEditDialog()">
                            <v-icon small>mdi-plus</v-icon>
                        </v-btn>
                    </div>
                </template>
                <v-card>
                    <v-card-title>
                        <span v-if="editedItem.ra">Editar aluno</span>
                        <span v-else>Novo aluno</span>
                    </v-card-title>
                    <v-card-text>
                        <v-row>
                            <v-col cols="12" sm="12">
                                <v-text-field v-model="editedItem.nome" label="Nome" maxlength="100"
                                    :rules="[rules.required]">
                                </v-text-field>
                            </v-col>
                            <v-col cols="12" sm="12">
                                <v-text-field v-model="editedItem.email" label="E-mail" maxlength="100"
                                    :rules="[rules.required, rules.email]"></v-text-field>
                            </v-col>
                            <v-col cols="12" sm="6">
                                <v-text-field v-mask="'###.###.###-##'" v-model="editedItem.cpf" label="CPF"
                                    :readonly=isDisabled :rules="[rules.required]"></v-text-field>
                            </v-col>
                            <v-col cols="12" sm="6">
                                <v-text-field v-model="editedItem.ra" label="Registro Acadêmico" :readonly=isDisabled
                                    maxlength="50" :rules="[rules.required]"></v-text-field>
                            </v-col>
                        </v-row>
                    </v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="blue darken-1" text @click="showEditDialog()">Cancelar</v-btn>
                        <v-btn color="blue darken-1" text @click="saveItem(editedItem)" :disabled=isFilledFields>Salvar
                        </v-btn>
                    </v-card-actions>
                </v-card>
            </v-dialog>
        </v-flex>
    </div>
</template>
<script>
import axios from "axios";

export default {
    nome: "AlunosGrid",
    created() {
        this.getAll();
    },
    data() {
        return {
            pageTitle: "Alunos",
            headers: [
                {
                    text: "Nome",
                    align: "start",
                    sortable: false,
                    value: "nome",
                },
                { text: "E-mail", value: "email" },
                { text: "CPF", value: "cpf" },
                { text: "Registro Acadêmico", value: "ra" },
                { text: "Ações", value: "actions", sortable: false },
            ],
            dialog: false,
            alunos: [],
            editedItem: {},
            isNew: false,
            rules: {
                required: value => !!value || 'O campo deve ser preenchido',
                email: value => {
                    const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
                    return pattern.test(value) || 'E-mail inválido'
                }
            }
        };
    },
    computed: {
        isDisabled() {
            return !this.isNew;
        },
        isFilledFields() {
            return !this.editedItem.nome ||
                !this.editedItem.ra ||
                !this.editedItem.cpf ||
                !this.editedItem.email;
        },
    },
    methods: {
        getAll() {
            var url = "https://localhost:44351/aluno/all";

            axios
                .get(url)
                .then((response) => {
                    this.alunos = response.data.map((item) => {
                        return {
                            ra: item.ra,
                            cpf: item.cpf,
                            nome: item.nome,
                            email: item.email,
                        };
                    });
                })
                .catch((error) => {
                    this.showErrors(error.response.data);
                });
        },
        showEditDialog(item) {
            this.isNew = !item;
            this.editedItem = item || {};
            this.dialog = !this.dialog;
            this.getAll();
        },
        saveItem(item) {
            var url = "https://localhost:44351/aluno";

            if (this.isNew) {
                axios
                    .post(url, {
                        nome: item.nome,
                        email: item.email,
                        ra: item.ra,
                        cpf: item.cpf
                    })
                    .then((response) => {
                        if (response) {
                            this.dialog = !this.dialog;
                            this.editedItem = {}
                            this.getAll();
                        }
                    })
                    .catch((error) => {
                        this.showErrors(error.response.data);
                    });
            } else {
                axios
                    .put(url, {
                        nome: item.nome,
                        email: item.email,
                        ra: item.ra
                    })
                    .then((response) => {
                        if (response) {
                            this.dialog = !this.dialog;
                            this.editedItem = {}
                            this.getAll();
                        }
                    })
                    .catch((error) => {
                        this.showErrors(error.response.data);
                    });
            }
        },
        deleteItem(item) {
            if (confirm("Confirma a exclusão do aluno " + item.nome + "?")) {
                var url = "https://localhost:44351/aluno/" + item.ra;

                axios
                    .delete(url)
                    .then((response) => {
                        if (response) {
                            this.getAll();
                        }
                    })
                    .catch((error) => {
                        this.showErrors(error.response.data);
                    });
            }
        },
        showErrors(errors) {
            var message = "";
            errors.forEach(element => {
                message += element.ErrorMessage + "\n";
            });
            alert(message);
        },
    },
};
</script>