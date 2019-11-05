node {
	stage('Checkout') {
		checkout scm
	}

	stage('Build'){
		bat 'nuget restore src/SQLSVN.sln'
		bat "\"${tool 'MSBuild'}\" src/SQLSVN.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
	}

	stage('Archive-kDbMigration'){
		archive 'kDbMigration/bin/Release/**'
	}

	stage('Archive-kDbRepair'){
		archive 'kDbRepair/bin/Release/**'
	}

}
