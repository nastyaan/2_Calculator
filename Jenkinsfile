node {
    stage('Hello') {
        echo 'Hello jenkins'
    }
    stage('Build Docker Image') {
        sh 'ls -l'
        sh 'docker compose build'
    }
    stage('Start Docker Container') {
        sh 'docker compose up -d'
    }
}
