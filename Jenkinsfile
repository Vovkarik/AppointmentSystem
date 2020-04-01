properties([disableConcurrentBuilds()])

pipeline {
    agent any

    options {
        buildDiscarder(logRotator(numToKeepStr:'10', artifactNumToKeepStr:'10'))
        timestamps()
    }

    environment {
       dotnet ='C:\\Program Files (x86)\\dotnet\\'
    }
    stages {
        stage ('Clone the project')
        {
            steps {
                git 'https://github.com/Vovkarik/AppointmentSystem.git'
            }
        }
        stage ('Restore packages')
        {
            steps {
                bat "dotnet restore AppointmentSystem\\AppointmentSystem.csproj"
            }
        }
        stage ('Clean')
        {
            steps {
                bat "dotnet clean AppointmentSystem\\AppointmentSystem.csproj"
            }
        }
        stage ('Build')
        {
            steps {
                bat "dotnet build AppointmentSystem\\AppointmentSystem.csproj --configuration Release"
            }
        }
        stage ('Publish')
        {
            steps {
                bat "dotnet publish AppointmentSystem\\AppointmentSystem.csproj"
            }
        }
    }
            
}