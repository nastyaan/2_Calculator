﻿pipeline {
    agent any

    stages {
        stage('Hello') {
            steps {
                echo 'Hello jenkins'
            }
        }
        stage('Build Docker Image') {
            steps {
                sh 'ls -l'
                sh 'docker compose build'
            }
        }
		stage('Start Docker Container') {
            steps {
                sh 'docker compose up -d'
            }
        }
    }
}
