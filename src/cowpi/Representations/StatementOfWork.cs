using Newtonsoft.Json;
using SharpYaml.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace cowpi.Representations
{
    public class StatementOfWork : List<StatementOfWork.Task>
    {
        public class WorkItem
        {
            public string Skill;
            public decimal Minutes;
        }

        public class Task
        {
            public string Description { get; set; }
            public List<string> RequiredSkills { get; set; }
            public List<WorkItem> EstimatedWork { get; set; }
            public List<WorkItem> CompletedWork { get; set; }
            public DateTime? DueDateTime { get; set; } = null;
            public string Status { get; set; }
        }

        public static StatementOfWork Load(TextReader reader)
        {
            var yamlStream = new YamlStream();
            yamlStream.Load(reader);

            var yamlDocument = yamlStream.Documents.First();

            var rootMap = (YamlMappingNode)yamlDocument.RootNode;
            var tasksArray = (YamlSequenceNode)rootMap.Children[new YamlScalarNode("statementOfWork")];

            var sow = new StatementOfWork();
            
            foreach(var taskNode in tasksArray.Children.Cast<YamlMappingNode>())
            {
                
                var task = new StatementOfWork.Task();
                foreach(var propNode in taskNode.Children)
                {
                    var name = ((YamlScalarNode)propNode.Key).ToString();

                    switch(name)
                    {
                        case "description":
                            task.Description = ((YamlScalarNode)propNode.Value).ToString();
                            break;
                        case "requiredSkills":
                            var skillList = ((YamlSequenceNode)propNode.Value);
                            task.RequiredSkills = new List<string>();
                            foreach (var item in skillList.Children)
                            {
                                task.RequiredSkills.Add(((YamlScalarNode)item).ToString());
                            }
                            break;
                        case "estimatedWork":
                            var workList = ((YamlSequenceNode)propNode.Value);
                            task.EstimatedWork = new List<WorkItem>();
                            foreach (var item in workList.Children.Cast<YamlMappingNode>())
                            {
                                var workItem = new WorkItem();
                                foreach (var workItemProperty in item.Children)
                                {
                                    var propName = ((YamlScalarNode)workItemProperty.Key).ToString();
                                    switch(propName)
                                    {
                                        case "skill":
                                            workItem.Skill = ((YamlScalarNode)workItemProperty.Value).ToString();
                                            break;
                                        case "minutes":
                                            workItem.Minutes = Decimal.Parse(((YamlScalarNode)workItemProperty.Value).ToString());
                                            break;
                                    }
                                }
                                task.EstimatedWork.Add(workItem);
                            }

                            break;

                        case "dueDateTime":
                            task.DueDateTime = DateTime.Parse(((YamlScalarNode)propNode.Value).ToString());
                            break;

                    }
                }

                sow.Add(task);
            }

            return sow;
        }

        public void Save(TextWriter writer)
        {

            var serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
            serializer.Serialize(writer, new { statementOfWork = this });

        }

        public static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings() {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        }; 
    }
}
