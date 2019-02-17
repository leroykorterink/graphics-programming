import AdvancedComponent from "../core/AdvancedComponent.js";
import makeLoadTexture from "../util/makeLoadTexture.js";

const loadTexture = makeLoadTexture(fileName => `assets/House/${fileName}`);

const House = (
  wallsWidth = 5,
  wallsHeight = 4,
  wallsDepth = 5,
  roofHeight = 3,
  roofOverhead = 0.85
) =>
  class House extends AdvancedComponent {
    constructor(scene) {
      super();

      this.init(scene);
    }

    async init(scene) {
      const walls = await this.createWalls(wallsWidth, wallsHeight, wallsDepth);
      const roof = await this.createRoof(roofHeight, roofOverhead, wallsHeight);
      const chimney = await this.createChimney(wallsHeight, roofHeight);
      const dormer = await this.createDormer(wallsHeight, roofHeight);

      const group = new THREE.Group();

      group.add(walls);
      group.add(roof);
      group.add(chimney);
      group.add(dormer);

      group.position.set(0, 2, -10);

      scene.add(group);
    }

    async createWalls(wallsWidth, wallsHeight, wallsDepth) {
      const textureScale = 3;

      const map = await loadTexture("wall_texture.jpg");
      map.wrapS = map.wrapT = THREE.RepeatWrapping;
      map.repeat.set(textureScale, textureScale);

      const normalMap = await loadTexture("wall_normal.jpg");
      normalMap.wrapS = normalMap.wrapT = THREE.RepeatWrapping;
      normalMap.repeat.set(textureScale, textureScale);

      const wallMaterial = new THREE.MeshStandardMaterial({ map, normalMap });

      const walls = new THREE.BoxBufferGeometry(
        wallsWidth,
        wallsHeight,
        wallsDepth
      );

      const wallsMesh = new THREE.Mesh(walls, wallMaterial);
      wallsMesh.castShadow = true;
      wallsMesh.receiveShadow = true;

      return wallsMesh;
    }

    async createRoof(roofHeight, roofOverhead, wallsHeight) {
      // normal roofOverhead = 0.705

      const textureScale = 3;

      const map = await loadTexture("roof_texture.jpg");
      map.wrapS = map.wrapT = THREE.RepeatWrapping;
      map.repeat.set(textureScale, textureScale);

      this.normalMap = await loadTexture("roof_normal.jpg");
      this.normalMap.repeat.set(textureScale, textureScale);

      const roofMaterial = new THREE.MeshStandardMaterial({
        map,
        normalMap: this.normalMap
      });

      const roof = new THREE.ConeBufferGeometry(
        wallsWidth * roofOverhead,
        roofHeight,
        4,
        1,
        false
      );

      const roofMesh = new THREE.Mesh(roof, roofMaterial);
      roofMesh.castShadow = true;
      roofMesh.receiveShadow = true;

      roofMesh.position.set(0, wallsHeight / 2 + roofHeight / 2, 0);
      roofMesh.rotation.set(0, 0.785, 0);

      return roofMesh;
    }

    async createChimney(wallsHeight, roofHeight) {
      const chimneyMaterial = new THREE.MeshLambertMaterial({
        color: 0x7f3026
      });

      const chimney = new THREE.BoxBufferGeometry(1, 3, 1);
      const chimneyMesh = new THREE.Mesh(chimney, chimneyMaterial);
      chimneyMesh.castShadow = true;
      chimneyMesh.receiveShadow = true;

      const smoke = this.smokeFactory();

      const chimneyGroup = new THREE.Group();
      chimneyGroup.add(chimneyMesh);
      chimneyGroup.add(smoke);

      chimneyGroup.position.set(-1, wallsHeight / 2 + roofHeight / 2, 0);

      return chimneyGroup;
    }

    async createDormer(wallsHeight, roofHeight) {

      const dormerWindowTexture = await loadTexture("window_texture.png");
      const sideWinowTexture = new THREE.MeshBasicMaterial( { color: 0x353535 } );            

      const dormerMaterial = [                                                      
        sideWinowTexture,
        sideWinowTexture,
        sideWinowTexture,
        sideWinowTexture,
        new THREE.MeshStandardMaterial( { map: dormerWindowTexture } ),                      
        sideWinowTexture,
      ];     

      const dormerRoofMaterial = new THREE.MeshLambertMaterial({
        color: 0x353535
      });

      const dormer = new THREE.BoxBufferGeometry(1.4, 1.4, 2);
      const dormerMesh = new THREE.Mesh(dormer, dormerMaterial);
      dormerMesh.position.set(0, -0.4, 0);

      const dormerRoof = new THREE.BoxBufferGeometry(1.6, 0.2, 2.2);
      const dormerRoofMesh = new THREE.Mesh(dormerRoof, dormerRoofMaterial);
      dormerRoofMesh.position.set(0, 0.4, 0);


      const dormerGroup = new THREE.Group();
      dormerGroup.add(dormerMesh);
      dormerGroup.add(dormerRoofMesh);

      dormerGroup.castShadow = true;
      dormerGroup.receiveShadow = true;

      dormerGroup.position.set(0, wallsHeight / 2 + roofHeight / 2, 1.5);

      return dormerGroup;
    }


    smokeFactory() {
      this.smokeMaterial = new THREE.MeshLambertMaterial({
        color: 0xffffff,
        transparent: true
        //flatShading: true
      });

      const smoke = new THREE.IcosahedronBufferGeometry(0.2);

      this.smokeMesh = new THREE.Mesh(smoke, this.smokeMaterial);

      this.resetSmoke(this.smokeMesh, this.smokeMaterial);

      return this.smokeMesh;
    }


    update() {
      if (!this.smokeMaterial || !this.smokeMesh || !this.normalMap) {
        return;
      }

      this.normalMap.rotation += (Math.PI / 180) * 45;

      if (this.smokeMaterial.opacity < 0)
        this.resetSmoke(this.smokeMesh, this.smokeMaterial);

      this.smokeMesh.position.set(
        (this.smokeMesh.position.x += 0.003),
        (this.smokeMesh.position.y += 0.003),
        (this.smokeMesh.position.z -= 0.005)
      );

      this.smokeMesh.scale.set(
        this.smokeMesh.scale.x + 0.003,
        this.smokeMesh.scale.y + 0.003,
        this.smokeMesh.scale.z + 0.003
      );

      this.smokeMaterial.opacity -= 0.001;
    }

    resetSmoke(smokeMesh, smokeMaterial) {
      smokeMesh.position.set(0, 1.5, 0);
      smokeMesh.scale.set(1, 1, 1);
      smokeMaterial.opacity = 1;
    }
  };

export default House;
