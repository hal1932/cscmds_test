#r "mscorlib.dll"
#r "D:\home\Documents\Visual Studio 2015\Projects\CsCmds\CsCmds\bin\Debug\CsCmds.dll"

using Autodesk.Maya.OpenMaya;
using CsCmds.Core;
using CsCmds.Dag;
using System.Linq;

var matInfo = Transform.CreateFrom(DependNode.FirstSelectedOrDefault()) // 選択されたDGNodeからDagNodeを生成
    ?.FirstShapeOrDefault() // このDagNode以下にある最初のシェイプを検索
    ?.FirstShadingEngineOrDefault() // このシェイプ以下にある最初のSGを検索
    ?.FirstDestinationNodeOrDefault(nodeObj => nodeObj.hasFn(MFn.Type.kMaterialInfo)); // このSGに接続されてる最初のMaterialInfoを検索
if (matInfo == null)
{
    Log.ErrorLine("MaterialInfoが見つかりませんでした");
    return;
}

var fileNodes = matInfo.EnumerateSourceNodes(nodeObj => nodeObj.hasFn(MFn.Type.kFileTexture))// MaterialInfoに接続されたFileノードを検索
    .Distinct() // 検索結果から重複を除去
    .Where(node => node.FindPlug("fileTextureName")?.GetValue<string>()?.Contains("COL") ?? false); // 名前にCOLを含むものを検索
if (fileNodes.Any()) // 検索結果が1つ以上存在したら
{
    foreach (var fileNode in fileNodes)
    {
        // 以下、fileNode に対する操作
        Log.WriteLine(fileNode.Name + "が見つかりました");
    }
}

